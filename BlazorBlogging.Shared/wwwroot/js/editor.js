window.editorInterop = {
    _scrollListeners: {},

    wrapSelection: function (elementId, before, after) {
        const el = document.getElementById(elementId);
        if (!el) return;
        const start = el.selectionStart;
        const end = el.selectionEnd;
        const text = el.value;
        const selected = text.substring(start, end);
        el.value = text.substring(0, start) + before + selected + after + text.substring(end);
        el.selectionStart = start + before.length;
        el.selectionEnd = start + before.length + selected.length;
        el.focus();
        el.dispatchEvent(new Event('input', { bubbles: true }));
    },

    insertText: function (elementId, text) {
        const el = document.getElementById(elementId);
        if (!el) return;
        const start = el.selectionStart;
        const val = el.value;
        el.value = val.substring(0, start) + text + val.substring(start);
        const newPos = start + text.length;
        el.selectionStart = newPos;
        el.selectionEnd = newPos;
        el.focus();
        el.dispatchEvent(new Event('input', { bubbles: true }));
    },

    insertLinePrefix: function (elementId, prefix) {
        const el = document.getElementById(elementId);
        if (!el) return;
        const start = el.selectionStart;
        const val = el.value;
        const lineStart = val.lastIndexOf('\n', start - 1) + 1;
        el.value = val.substring(0, lineStart) + prefix + val.substring(lineStart);
        const newPos = start + prefix.length;
        el.selectionStart = newPos;
        el.selectionEnd = newPos;
        el.focus();
        el.dispatchEvent(new Event('input', { bubbles: true }));
    },

    insertBlock: function (elementId, blockText, cursorOffset) {
        const el = document.getElementById(elementId);
        if (!el) return;
        const start = el.selectionStart;
        const end = el.selectionEnd;
        const val = el.value;
        const selected = val.substring(start, end);

        let toInsert;
        if (selected.length > 0) {
            toInsert = blockText.replace('{{selection}}', selected);
        } else {
            toInsert = blockText.replace('{{selection}}', '');
        }

        const needsNewlineBefore = start > 0 && val[start - 1] !== '\n';
        const prefix = needsNewlineBefore ? '\n' : '';

        el.value = val.substring(0, start) + prefix + toInsert + val.substring(end);
        const newPos = start + prefix.length + cursorOffset;
        el.selectionStart = newPos;
        el.selectionEnd = newPos;
        el.focus();
        el.dispatchEvent(new Event('input', { bubbles: true }));
    },

    initScrollSync: function (editorId, previewId) {
        const editor = document.getElementById(editorId);
        const preview = document.getElementById(previewId);
        if (!editor || !preview) return;

        // Clean up any existing listener
        this.disposeScrollSync(editorId);

        let ticking = false;
        const handler = function () {
            if (!ticking) {
                requestAnimationFrame(function () {
                    const editorMax = editor.scrollHeight - editor.clientHeight;
                    if (editorMax > 0) {
                        const ratio = editor.scrollTop / editorMax;
                        const previewMax = preview.scrollHeight - preview.clientHeight;
                        preview.scrollTop = ratio * previewMax;
                    }
                    ticking = false;
                });
                ticking = true;
            }
        };

        editor.addEventListener('scroll', handler, { passive: true });
        this._scrollListeners[editorId] = { editor: editor, handler: handler };
    },

    disposeScrollSync: function (editorId) {
        const entry = this._scrollListeners[editorId];
        if (entry) {
            entry.editor.removeEventListener('scroll', entry.handler);
            delete this._scrollListeners[editorId];
        }
    }
};
