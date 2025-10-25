import { RibbonManager } from "./RibbonManager.js";
import { RibbonCommandHandler } from "./RibbonCommandHandler.js";
import { RibbonGroupModel } from "./RibbonGroupModel.js";
import { RibbonItemModel } from "./RibbonItemModel.js";
import { renderRibbon } from "./RibbonDOMRenderer.js";

const manager = new RibbonManager();

const runHandler = new RibbonCommandHandler("run", () => true, () => alert("Running macro!"));
const stopHandler = new RibbonCommandHandler("stop", () => false, () => alert("Stopped."));

manager.bindCommand("run", runHandler);
manager.bindCommand("stop", stopHandler);

const group = new RibbonGroupModel({
  id: "main",
  title: "Main Controls",
  items: [
    new RibbonItemModel({ id: "runBtn", type: "button", label: "Run", commandId: "run", order: 1 }),
    new RibbonItemModel({ id: "stopBtn", type: "button", label: "Stop", commandId: "stop", order: 2, enabled: false })
  ]
});

manager.registerGroup(group);

document.addEventListener("DOMContentLoaded", () => {
  const container = document.getElementById("ribbon");
  renderRibbon(manager, container);
});