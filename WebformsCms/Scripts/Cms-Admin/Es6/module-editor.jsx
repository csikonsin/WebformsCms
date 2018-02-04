import React from "react"
import EventEmitter from "./event-emitter"
import axios from "axios"

class ModuleButton extends React.Component {
    constructor(props){
        super(props);
    }

    render() {
        return (
            <div className="module-button" onClick={this.handleAddModule.bind(this)}>
                <label>{this.props.name}</label>
                <div className="button-inner">                
                </div>
            </div>
        )
    }
    
    handleAddModule(){
        EventEmitter.dispatch("add-module",{
            caller: this.props.caller,
            type: this.props.type,
            menuId: this.props.menuId,
            moduleId: this.props.moduleId
        })
    }
}


export default class ModuleEditor extends React.Component {

    constructor(props) {
        super(props)

        this.state = {
            modules: [],
            search: ""
        }
    }

    componentDidMount(){
        this.setState({
        modules: [
            {
                type: 0,
                name: "Text"
            },
            {
                type: 1,
                name: "Überschrift"
            },
            {
                type: 2,
                name :"Bild"
            }
        ]});
    }

    getModules(){
        return this.state.modules.filter((module) => module.name.indexOf(this.state.search) > -1);
    }

    
    handleSearchChange(e){
        this.setState({
            search: e.target.value
        })
    }

    handleDeleteModule(e){
        EventEmitter.dispatch("delete-module", {
            caller: this.props.caller,
            menuId: this.props.menuId,
            moduleId: this.props.moduleId
        })
        e.preventDefault();   
    }

    handleClose(e){        
        let close = this.props.closeEditor.bind(this.props.parent);
        close();
        e.preventDefault();
    }

    render() {

        var list = this.getModules().map((module) => {                            
            return <ModuleButton type={module.type} name={module.name} key={module.type} caller={this.props.caller} menuId={this.props.menuId} moduleId={this.props.moduleId} />
        })
        if(list.length===0){
            list = <div className="empty-message">Kein Modul gefunden!</div>
        }

        var display;

        if(this.props.isEdit){
            
        }else if(this.props.isDelete){
            display = <div className="module-delete">
                        <span>Wirklich löschen?</span>
                        <button onClick={this.handleDeleteModule.bind(this)}>Ja</button>
                        <button onClick={this.handleClose.bind(this)}>Nein</button>
                      </div>
        }
        else{
            display = <div className="module-list">
                        <input ref={(search) => this.search = search} className="name-filter" type="search" onChange={this.handleSearchChange.bind(this)} placeholder="Modul suchen" />
                        <div className="scroll">                        
                        {list}
                        </div>
                      </div>
        }

        return(
            <div className="module-editor">
                <div className="wrapper">                    
                    {display}
                </div>
            </div>
        )
    }
}