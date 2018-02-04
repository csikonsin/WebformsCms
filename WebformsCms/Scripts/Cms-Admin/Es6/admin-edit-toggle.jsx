import React from "react"
import axios from "axios"
export default class AdminEditToggle extends React.Component{
    constructor(props){
        super(props)
        this.state = {

        }
    }

    render(){
        var wrapperClass ="wrapper";
        if(this.props.isEdit){
            wrapperClass += " active"
        }

        return (
            <div className={wrapperClass} onClick={this.handleToggle.bind(this)}>
                <span>Toggle Edit</span>
                <i className="fas fa-edit"></i>
            </div>
        )
    }

    handleToggle() {
        axios.post("/Api/Cms.asmx/ToggleEdit").then((res)=>{
            document.location.reload();
        }).catch((e)=>{

        })
    }
}