import { Compartment } from '@codemirror/state'
import { EditorView } from '@codemirror/view'

export const themeCompartment = new Compartment()

export const editorThemeBase = EditorView.theme({
  '&': {
    background: 'transparent !important',
    fontSize: '16px'
  },
  '&.cm-focused': {
    outline: 'none'
  }
})

export const editorThemeDark = EditorView.theme({
  '&': {
    color: '#fff',
  },
  '&.cm-focused .cm-cursor': {
    borderLeftColor: '#fff'
  },
  '.cm-content': {
    caretColor: '#fff'
  }
})

export const editorThemeLight = EditorView.theme({
  '&': {
    color: '#000',
  },
  '&.cm-focused .cm-cursor': {
    borderLeftColor: '#000'
  },
  '.cm-content': {
    caretColor: '#000'
  }
})
