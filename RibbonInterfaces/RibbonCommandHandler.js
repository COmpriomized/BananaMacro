export class RibbonCommandHandler {
  constructor(id, canExecuteFn, executeFn) {
    this.id = id;
    this.canExecute = canExecuteFn || (() => true);
    this.execute = executeFn || (() => {});
  }
}