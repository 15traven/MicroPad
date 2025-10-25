import React, { useCallback, useEffect } from 'react'
import { EditorState } from '@codemirror/state'
import { githubDark, githubLight } from '@uiw/codemirror-theme-github'
import useCodeMirror, { themeCompartment } from './use-codemirror'
import './styles/editor.css'

interface Props {
  initialDoc: string,
  onChange: (doc: string) => void
}

const Editor: React.FC<Props> = (props) => {
  const { onChange, initialDoc } = props
  const handleChange = useCallback(
    (state: EditorState) => onChange(state.doc.toString()),
    [onChange]
  )
  const [refContainer, editorView] = useCodeMirror<HTMLDivElement>({
    initialDoc: initialDoc,
    onChange: handleChange
  })

  useEffect(() => {
    globalThis.setEditorTheme = function(theme: 'light' | 'dark') {
      if (editorView) {
        const themeExtension = theme === 'dark' ? githubDark : githubLight

        editorView.dispatch({
          effects: themeCompartment.reconfigure(themeExtension)
        })
      }
    }
  }, [editorView])

  return <div className='editor-wrapper' ref={refContainer}></div>
}

export default Editor
