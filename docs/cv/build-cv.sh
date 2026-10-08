#!/usr/bin/env sh
# Regenerates wwwroot/George-CV.pdf from docs/cv/George-CV.html using headless Edge (or Chrome),
# then bumps the ?v= cache-buster in Data/PortfolioContent.cs so browsers fetch the new file.
set -e
cd "$(dirname "$0")/../.."

for b in "/c/Program Files (x86)/Microsoft/Edge/Application/msedge.exe" \
         "/c/Program Files/Microsoft/Edge/Application/msedge.exe" \
         "/c/Program Files/Google/Chrome/Application/chrome.exe" \
         "$(command -v google-chrome || true)" "$(command -v chromium || true)"; do
    [ -n "$b" ] && [ -f "$b" ] && BROWSER="$b" && break
done
[ -n "$BROWSER" ] || { echo "No Edge/Chrome found" >&2; exit 1; }

SRC="file:///$(pwd -W 2>/dev/null || pwd)/docs/cv/George-CV.html"
OUT="$(pwd -W 2>/dev/null || pwd)/wwwroot/George-CV.pdf"
PROFILE="$(mktemp -d)"
trap 'rm -rf "$PROFILE"' EXIT

"$BROWSER" --headless=new --disable-gpu --user-data-dir="$PROFILE" --no-pdf-header-footer \
    --virtual-time-budget=10000 --print-to-pdf="$OUT" "$SRC"
echo "Wrote $OUT"

# Version = first 8 chars of the PDF's hash: changes only when the content does.
VERSION="$(sha256sum wwwroot/George-CV.pdf | cut -c1-8)"
sed -i "s/George-CV\.pdf?v=[^\"]*\"/George-CV.pdf?v=$VERSION\"/" Data/PortfolioContent.cs
echo "CvPath version set to $VERSION"
