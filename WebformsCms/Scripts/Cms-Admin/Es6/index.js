import React from "react"
import ReactDOM from "react-dom"
import ModuleEditAdd from "./module-editadd.jsx"
import CmsEditor from "./cms-editor.jsx"
import MenuAdd from "./menu-add.jsx"

 document.querySelectorAll(".module-editadd-root").forEach((element) => {    
    var moduleId = element.getAttribute("data-moduleid");
    var menuId = element.getAttribute("data-menuid");
    var moduleType = element.getAttribute("data-moduletype");
    var parentModuleId = element.getAttribute("data-parentmoduleid");
    ReactDOM.render(<ModuleEditAdd moduleId={moduleId} menuId={menuId} moduleType={moduleType} parentModuleId={parentModuleId} />,element)
 })



 document.querySelectorAll(".menu-add-root").forEach((element) => {
    var menuControlId = element.getAttribute("data-controlid");
    var parentMenuId = element.getAttribute("data-parentmenuid");
    ReactDOM.render(<MenuAdd parentControlId={menuControlId} parentMenuId={parentMenuId} />, element)
 })


ReactDOM.render(<CmsEditor/>, document.getElementById("module-editor-root"))
