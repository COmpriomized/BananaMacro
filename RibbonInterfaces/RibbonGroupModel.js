export class RibbonGroupModel {
  constructor({ id, title, description, items = [], visible = true, order = 0, meta = {} }) {
    this.id = id;
    this.title = title;
    this.description = description;
    this.items = items;
    this.visible = visible;
    this.order = order;
    this.meta = meta;
  }
}