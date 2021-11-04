mergeInto(LibraryManager.library, {
    OnUnityInspect: function (n) {
        if (!n) return;
        const t = new CustomEvent("unity-inspect", {
            detail: n
        });
        window.dispatchEvent(t)
    },
    OnUnityUninspect: function () {
        const n = new CustomEvent("unity-uninspect");
        window.dispatchEvent(n)
    },
    EmptyInspect: function (n) {
        const t = new CustomEvent("unity-inspect", {
            detail: n
        });
        window.dispatchEvent(t)
    },
    OnUnityPause: function () {
        const n = new CustomEvent("unity-pause");
        window.dispatchEvent(n)
    },
    OnUnityUnpause: function () {
        const n = new CustomEvent("unity-unpause");
        window.dispatchEvent(n)
    }
});