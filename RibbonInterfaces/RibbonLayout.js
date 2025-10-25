export class RibbonLayout {
  constructor(groups = []) {
    this.groups = groups;
    this.lastUpdated = new Date().toISOString();
  }
}