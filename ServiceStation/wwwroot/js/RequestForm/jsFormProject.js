(function () {

    var groups = [
        { cb: '.project-type-checkbox', target: 'textproject_types' },
        { cb: '.Obj-checkbox', target: 'textobjectives' },
        { cb: '.attachment-checkbox', target: 'textattachment_types' },
        { cb: '.RELATED_SYSTEM-checkbox', target: 'textrelated_systems' }
    ];

    function isOther(cb) {
        var v = (cb.value || '').trim().toUpperCase();
        var c = (cb.dataset.code || '').trim().toUpperCase();
        return v === 'OTHER' || c === 'OTHER';
    }

    // หาช่อง "อื่นๆ" จาก label เดียวกับ checkbox
    function getOtherInput(cb) {
        var label = cb.closest('label');
        return label ? label.querySelector('input[type="text"]') : null;
    }

    function showHide(input, show, clear) {
        if (!input) return;
        input.style.setProperty('display', show ? 'inline-block' : 'none', 'important');
        if (!show && clear) input.value = '';
    }

    function syncGroup(g, clearOther) {
        var checkboxes = document.querySelectorAll(g.cb);
        if (checkboxes.length === 0) return;

        var values = [];

        checkboxes.forEach(function (cb) {
            if (cb.checked) values.push(cb.value);
            if (isOther(cb)) showHide(getOtherInput(cb), cb.checked, clearOther);
        });

        var targetInput = document.getElementById(g.target);
        if (targetInput) {
            targetInput.value = values.length > 0 ? '|' + values.join('|') + '|' : '';
        }
    }

    function syncAll(clearOther) {
        groups.forEach(function (g) { syncGroup(g, clearOther); });
    }

    window.svcSyncAll = syncAll;

    if (!window.__svcDelegated) {
        window.__svcDelegated = true;
        document.addEventListener('change', function (e) {
            var t = e.target;
            if (!t || !t.matches || !t.matches('input[type="checkbox"]')) return;
            groups.forEach(function (g) {
                if (t.matches(g.cb)) syncGroup(g, true);
            });
        });
    }

    syncAll(false);

})();