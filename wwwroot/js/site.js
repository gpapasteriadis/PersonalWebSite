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

    // --- Diagrams (Mermaid) ---------------------------------------------------------------
    // Loaded from the CDN on first use, so pages without diagrams don't pay for it.
    const mermaidUrl = 'https://cdn.jsdelivr.net/npm/mermaid@12.1.0/dist/mermaid.esm.min.mjs';
    let mermaidModule;
    let diagramCount = 0;

    const loadMermaid = () => mermaidModule ??= import(mermaidUrl).then(m => m.default);

    const nextFrame = () => new Promise(resolve => requestAnimationFrame(() => requestAnimationFrame(resolve)));

    // MudBlazor exposes colors as "rgba(r,g,b,a)" or "r,g,b". Mermaid derives shades from fills and
    // ignores alpha, so tints are blended into solid colors here.
    const channels = value => (value.match(/[\d.]+/g) || []).slice(0, 3).map(Number);
    const blend = (color, base, amount) => {
        const [a, b] = [channels(color), channels(base)];
        return `rgb(${a.map((c, i) => Math.round(c * amount + b[i] * (1 - amount))).join(', ')})`;
    };

    // Theme variables come from the live MudBlazor palette, so diagrams match light and dark mode.
    const diagramTheme = () => {
        const css = getComputedStyle(root);
        const v = name => css.getPropertyValue(name).trim();
        const isDark = root.dataset.theme === 'dark';
        return {
            darkMode: isDark,
            fontFamily: 'Roboto, "Helvetica Neue", Arial, sans-serif',
            fontSize: '15px',
            background: v('--mud-palette-surface'),
            primaryColor: v('--mud-palette-background'),
            primaryTextColor: v('--mud-palette-text-primary'),
            primaryBorderColor: v('--mud-palette-primary'),
            secondaryColor: v('--mud-palette-background-gray'),
            tertiaryColor: v('--mud-palette-surface'),
            lineColor: v('--mud-palette-text-secondary'),
            textColor: v('--mud-palette-text-primary'),
            clusterBkg: blend(v('--mud-palette-primary-rgb'), v('--mud-palette-background'), isDark ? .07 : .06),
            clusterBorder: v(isDark ? '--mud-palette-tertiary' : '--mud-palette-secondary'), // 3:1 on light
            edgeLabelBackground: v('--mud-palette-surface'),
        };
    };

    const renderNow = async (element, source, errorText) => {
        if (!element) return;
        try {
            const mermaid = await loadMermaid();
            await nextFrame(); // let a theme change reach the CSS variables first
            mermaid.initialize({
                startOnLoad: false,
                securityLevel: 'strict',
                theme: 'base',
                themeVariables: diagramTheme(),
                flowchart: { curve: 'basis', htmlLabels: true },
            });
            const { svg } = await mermaid.render(`diagram-${++diagramCount}`, source);
            element.innerHTML = svg;
            element.setAttribute('role', 'img');
            element.classList.remove('diagram__canvas--error');
        } catch (error) {
            console.warn('Diagram could not be rendered', error);
            element.textContent = errorText || 'The diagram could not be loaded.';
            element.removeAttribute('role'); // let screen readers read the error text
            element.classList.add('diagram__canvas--error');
        }
    };

    // mermaid.initialize/render share global state, so diagrams render one at a time.
    let renderQueue = Promise.resolve();
    const renderDiagram = (element, source, errorText) =>
        renderQueue = renderQueue.then(() => renderNow(element, source, errorText));

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
        renderDiagram,
        // Called by pages after their first render, so /page#section links land on the section.
        scrollToHash() {
            const id = decodeURIComponent(location.hash.slice(1));
            if (!id) return;
            document.getElementById(id)?.scrollIntoView({ behavior: reduceMotion ? 'auto' : 'smooth' });
        }
    };
})();
