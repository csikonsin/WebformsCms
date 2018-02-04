import React from "react"
import EventEmitter from "./event-emitter.js"
import MenuEditor from "./menu-editor.jsx"
import ModuleEditor from "./module-editor.jsx"

export default class CmsEditor extends React.Component {
    constructor(props) {
        super(props)

        let instance = this;

        EventEmitter.subscribe("open-add-menu", function({caller, parentControlId, parentMenuId}) {
            instance.openEditor()
            instance.setState({
                open:true,
                caller: caller,
                tab: <MenuEditor parentControlId={parentControlId} parentMenuId={parentMenuId}/>
            });
        })

        EventEmitter.subscribe("open-add-module", function({caller, menuId, moduleId, isEdit, isDelete}) {            
            instance.openEditor()
            instance.setState({
                open:true,                
                tab: <ModuleEditor caller={caller} menuId={menuId} moduleId={moduleId} isEdit={isEdit} isDelete={isDelete} parent={instance} closeEditor={instance.closeEditor}/>
            });
        })

        EventEmitter.subscribe("close-editor", function() {            
            instance.closeEditor();
            instance.setState({
                open:false,                
                tab: null
            });
        })

        this.state = {
            modules: [],
            open: false,
            search: "",
            tab: null
        }
    }

 

    render() {
        return (
            <div id="cms-editor" className={this.state.open ? 'open' : ''} ref={(editor) => this.editor = editor}>
                {this.state.tab}
                <a className="button close" href="javascript:void(0)" onClick={this.closeEditor.bind(this)}></a>
            </div>
        )
    }

    openEditor() {
        if(this.state.open)return;
        this.editor.ownerDocument.body.className += "cms-editor-open";
    }

    closeEditor() {
        if(!this.state.open)return;
        this.setState({open:false});
        this.editor.ownerDocument.body.className = this.editor.ownerDocument.body.className.replace("cms-editor-open", "");
    }
    openNewModule() {
        this.search.focus();
    }

    openNewMenu() {

    }

 
}


