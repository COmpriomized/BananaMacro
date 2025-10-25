export class RibbonItemModel {
  constructor({ id, type, label, tooltip, icon, commandId, enabled = true, visible = true, order = 0, meta = {} }) {
    this.id = id;
    this.type = type;
    this.label = label;
    this.tooltip = tooltip;
    this.icon = icon;
    this.commandId = commandId;
    this.enabled = enabled;
    this.visible = visible;
    this.order = order;
    this.meta = meta;
  }
}