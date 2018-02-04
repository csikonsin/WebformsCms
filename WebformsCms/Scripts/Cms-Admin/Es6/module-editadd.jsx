import React from "react"
import EventEmitter from "./event-emitter.js"
import axios from "axios"

export default class ModuleEditAdd extends React.Component {

    constructor(props){
        super(props);

        let instance = this;
        EventEmitter.subscribe("add-module", function({caller,type , menuId, moduleId}) {
            if(caller != instance)return;
            
            instance.addModule(type, menuId, moduleId);
        })

        EventEmitter.subscribe("delete-module", function({caller, menuId, moduleId}) {
            if(caller != instance)return;
            
            instance.deleteModule( menuId, moduleId);
        })

        this.state = {
            newModule: null,
            deleted: false
        }
    }

    render() {

        var type;

        if(this.props.moduleId==0){
            type = <a className="button add-modoule" onClick={this.handleOpen.bind(this)}>Neues Modul hinzufügen</a>
        }else{
            type = <div className="editdel-module">
                        <div className="wrapper">
                            <a className="button edit-modoule" onClick={this.handleEdit.bind(this)}>Bearbeiten</a>
                            <a className="button del-module" onClick={this.handleDelete.bind(this)}>Löschen</a>
                        </div>
                   </div>
        }

        var display = <div className="wrapper" ref={(wrapper) => this.wrapper = wrapper}>{type}</div>
        if(this.state.deleted) display=null;
        return display
    }

    handleEdit(){
        this.handleOpen({
            isEdit:true
        })
    }

    handleDelete(){
        this.handleOpen({
            isDelete:true
        })
    }

    handleOpen({isDelete,isEdit}) {
        EventEmitter.dispatch("open-add-module", {
            caller: this,
            menuId: this.props.menuId,
            moduleId: this.props.moduleId,
            isEdit: isEdit,
            isDelete: isDelete
        })
    }

    addModule(type, menuId, moduleId){
        axios.post("/api/Modules.asmx/NewModule", {
            menuId: menuId,
            moduleType: type,
            parentId: moduleId
        }).then(function(resp){
            //this.wrapper.ownerDocument.location.reload();
        }).catch(function(error){
            console.error("Error adding module!");
        })

    }


    deleteModule(menuId, moduleId){
        axios.post("/Api/Modules.asmx/DeleteModule", {
            menuId: this.props.menuId,
            moduleId: this.props.moduleId
        }).then((r)=>{
            this.wrapper.parentElement.previousSibling.remove()
            this.setState({deleted:true})
            EventEmitter.dispatch("close-editor", null);
        }).catch((e)=>{
            console.error(e);
        })
    }
}

