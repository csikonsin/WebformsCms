import React from "react";
import EventEmitter from "./event-emitter"

export default class MenuAdd extends React.Component{
    constructor(props){
        super(props);
        this.state = {

        }
    }

    render() {
        return(
            <div className="wrapper">
                <div className="button add-menu" onClick={this.addMenu.bind(this)}>Neuen Menüpunkt hinzufügen</div>
            </div>
        )
    }

    addMenu() {        
        EventEmitter.dispatch("open-add-menu",{
            caller: this,
            parentControlId: this.props.parentControlId,
            parentMenuId: this.props.parentMenuId
        })
    }

}