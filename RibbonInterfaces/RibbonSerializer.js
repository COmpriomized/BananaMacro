export class RibbonSerializer {
  static serialize(layout) {
    return JSON.stringify(layout, null, 2);
  }

  static deserialize(json) {
    try {
      return JSON.parse(json);
    } catch {
      return null;
    }
  }
}