import React from "react"
import ReactDOM from "react-dom"
import ModuleEditAdd from "./module-editadd.jsx"
import CmsEditor from "./cms-editor.jsx"
import MenuAdd from "./menu-add.jsx"
import EventsEmitter from "./event-emitter"
import AdminEditToggle from "./admin-edit-toggle.jsx"

let loadEditadd = function(loadNew){

    var query = ".module-editadd-root";
    if(loadNew){
        query +=".notloaded";
    }

    document.querySelectorAll(query).forEach((element) => {    
        var moduleId = element.getAttribute("data-moduleid");
        var menuId = element.getAttribute("data-menuid");
        var moduleType = element.getAttribute("data-moduletype");
        var parentModuleId = element.getAttribute("data-parentmoduleid");
        ReactDOM.render(<ModuleEditAdd moduleId={moduleId} menuId={menuId} moduleType={moduleType} parentModuleId={parentModuleId} />,element)

        if(loadNew){
            element.className = element.className.replace(" notloaded", "");
        }
    })
}
loadEditadd()

 EventsEmitter.subscribe("load-new-editadd", function(){
    loadEditadd(true)
 })
 



 document.querySelectorAll(".menu-add-root").forEach((element) => {
    var menuControlId = element.getAttribute("data-controlid");
    var parentMenuId = element.getAttribute("data-parentmenuid");
    ReactDOM.render(<MenuAdd parentControlId={menuControlId} parentMenuId={parentMenuId} />, element)
 })


ReactDOM.render(<CmsEditor/>, document.getElementById("module-editor-root"))

var editRoot = document.getElementById("admin-edit-toggle-root");
var isEdit = (editRoot.getAttribute("data-isedit").toLowerCase() == "true");
ReactDOM.render(<AdminEditToggle isEdit={isEdit}/>, editRoot)
