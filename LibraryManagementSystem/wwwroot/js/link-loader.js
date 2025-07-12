document.addEventListener("DOMContentLoaded", () => {
    const contentArea = document.getElementById("contentArea");

    document.getElementById("dynamicLinks")
        .addEventListener("click", async (evt) => {
            const link = evt.target.closest("a[data-partial='true']");
            if (!link) return;                // not one of our partial links

            evt.preventDefault();             // no full page reload

            // visual cue (optional)
            link.classList.add("disabled");

            try {
                const resp = await fetch(link.href, { headers: { "X-Requested-With": "XMLHttpRequest" } });
                if (!resp.ok) throw new Error(`HTTP ${resp.status}`);
                contentArea.innerHTML = await resp.text();
                window.history.pushState({}, "", link.href); // update URL (optional)
            } catch (err) {
                contentArea.innerHTML =
                    `<div class="alert alert-danger">Failed to load: ${err}</div>`;
            } finally {
                link.classList.remove("disabled");
            }
        });
});