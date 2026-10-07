// All JavaScript for the site. Blazor calls into window.site through IJSRuntime (see SharedState/UiState.cs).
// Loaded before blazor.webassembly.js so preferences can be read before the first render.
(() => {
    'use strict';

    const keys = { culture: 'BlazorCulture', darkMode: 'DarkModeIsOn' };
    const root = document.documentElement;
    const reduceMotion = window.matchMedia('(prefers-reduced-motion: reduce)').matches;

    const storage = {
        get(key) {
            try { return localStorage.getItem(key); } catch { return null; }
        },
        set(key, value) {
            try { localStorage.setItem(key, value); } catch { /* storage unavailable (private mode) */ }
        }
    };

    const readDarkMode = () => {
        const stored = storage.get(keys.darkMode);
        if (stored !== null) return stored === 'true';
        return window.matchMedia('(prefers-color-scheme: dark)').matches;
    };

    const applyTheme = isDark => { root.dataset.theme = isDark ? 'dark' : 'light'; };

    // Apply the theme immediately so the loading screen matches the app.
    applyTheme(readDarkMode());

    // --- App bar: hide when scrolling down, show when scrolling up -------------------------
    let lastScrollY = window.scrollY;
    window.addEventListener('scroll', () => {
        const appBar = document.getElementById('appbar');
        const y = window.scrollY;
        if (appBar) appBar.dataset.hidden = String(y > lastScrollY && y > 120);
        lastScrollY = y;
    }, { passive: true });

    // --- Scroll reveal + scrollspy -------------------------------------------------------
    // Blazor renders after this script runs, so a MutationObserver picks up new elements.
    const supportsObserver = 'IntersectionObserver' in window;
    if (supportsObserver && !reduceMotion) root.classList.add('js-reveal');

    const revealObserver = supportsObserver && new IntersectionObserver((entries, observer) => {
        for (const entry of entries) {
            if (!entry.isIntersecting) continue;
            entry.target.classList.add('is-visible');
            observer.unobserve(entry.target);
        }
    }, { threshold: 0.05, rootMargin: '0px 0px -40px 0px' });

    const setActiveNav = id => {
        for (const link of document.querySelectorAll('[data-nav-target]')) {
            if (link.dataset.navTarget === id) link.setAttribute('aria-current', 'true');
            else link.removeAttribute('aria-current');
        }
    };

    const spyObserver = supportsObserver && new IntersectionObserver(entries => {
        for (const entry of entries) {
            if (entry.isIntersecting) setActiveNav(entry.target.id);
        }
    }, { rootMargin: '-45% 0px -50% 0px' });

    // Tracked so elements Blazor removes (e.g. navigating to another page) are unobserved and can be collected.
    const revealed = new Set();
    const spied = new Set();

    const syncObservers = () => {
        for (const [set, observer] of [[revealed, revealObserver], [spied, spyObserver]]) {
            for (const el of set) {
                if (el.isConnected) continue;
                observer.unobserve(el);
                set.delete(el);
            }
        }
        for (const el of document.querySelectorAll('.reveal:not(.is-visible)')) {
            if (revealed.has(el)) continue;
            revealed.add(el);
            revealObserver.observe(el);
        }
        for (const section of document.querySelectorAll('section[id]')) {
            if (spied.has(section)) continue;
            spied.add(section);
            spyObserver.observe(section);
        }
    };

    // DOM mutations come in bursts (Blazor renders, ripples, snackbars); scan at most once per frame.
    let syncQueued = false;
    if (supportsObserver) {
        new MutationObserver(() => {
            if (syncQueued) return;
            syncQueued = true;
            requestAnimationFrame(() => { syncQueued = false; syncObservers(); });
        }).observe(document.body, { childList: true, subtree: true });
    }

    // --- API used from Blazor ------------------------------------------------------------
    window.site = {
        prefs: {
            get: () => ({ culture: storage.get(keys.culture), darkMode: readDarkMode() })
        },
        setCulture(name) {
            storage.set(keys.culture, name);
            root.lang = name;
        },
        setDarkMode(isDark) {
            storage.set(keys.darkMode, String(isDark));
            applyTheme(isDark);
        },
        scrollToTop() {
            history.replaceState(null, '', location.pathname);
            window.scrollTo({ top: 0, behavior: reduceMotion ? 'auto' : 'smooth' });
            setActiveNav(null);
        },
        // Called by the home page after its first render, so /#section links from other pages land on the section.
        scrollToHash() {
            const id = decodeURIComponent(location.hash.slice(1));
            if (!id) return;
            document.getElementById(id)?.scrollIntoView({ behavior: reduceMotion ? 'auto' : 'smooth' });
        }
    };
})();
