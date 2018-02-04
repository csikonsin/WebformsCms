import React from "react"
import EventEmitter from "./event-emitter.js"
import axios from "axios"

export default class MenuEditor extends React.Component {
    constructor(props){
        super(props)

        this.state = {
            name: "",
            relativeUrl: "",
            homepage: false
        }
    }
    
    render() {
        return(
            <div className="menu-editor" ref={(wrapper) => this.wrapper = wrapper}>
                <h1>Add new menu</h1>
                <fieldset>
                    <div className="row">
                        <label>Menü name</label>
                        <input value={this.state.name} onChange={this.handleNameChange.bind(this)} type="text" />
                    </div>
                    <div className="row">
                        <label>Relative Url</label>
                        <input value={this.state.relativeUrl} onChange={this.handleRelativeUrlChange.bind(this)} type="text"/>
                    </div>
                    <div className="row">
                        <label htmlFor="start">Startseite</label>
                        <input type="checkbox" name="start" onChange={this.handleHomepageChange.bind(this)}/>
                    </div>
                    <div className="row">
                        <button onClick={this.handleAddMenu.bind(this)}>Hinzufügen</button>
                    </div>
                </fieldset>
            </div>
        )
    }

    handleHomepageChange(e){
        this.setState({
            homepage: e.target.checked
        })
    }

    handleNameChange(e){
        this.setState({
            name: e.target.value
        })
    }

    handleRelativeUrlChange(e){
        this.setState({
            relativeUrl: e.target.value
        })
    }

    handleAddMenu() {
        axios.post("/api/Menus.asmx/MenuAddEdit", {
            menuId: this.props.menuId || 0,
            parentControlId: this.props.parentControlId,            
            name: this.state.name,
            relativeUrl: this.state.relativeUrl,
            homepage: this.state.homepage,
            parentId: this.props.parentMenuId || 0
        }).then(function(resp){
            //this.wrapper.ownerDocument.location.reload();
        }).catch(function(error){
            console.error("Error adding menu!");
        })

    }
}