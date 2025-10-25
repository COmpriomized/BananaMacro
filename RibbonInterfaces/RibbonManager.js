export class RibbonManager {
  constructor() {
    this.groups = new Map();
    this.handlers = new Map();
    this.listeners = [];
  }

  registerGroup(group) {
    this.groups.set(group.id, { ...group, items: [...group.items] });
  }

  unregisterGroup(groupId) {
    this.groups.delete(groupId);
  }

  getGroups() {
    return Array.from(this.groups.values()).sort((a, b) => (a.order || 0) - (b.order || 0));
  }

  bindCommand(commandId, handler) {
    this.handlers.set(commandId, handler);
  }

  unbindCommand(commandId) {
    this.handlers.delete(commandId);
  }

  async invokeCommand(commandId, parameter) {
    const handler = this.handlers.get(commandId);
    if (!handler || !handler.canExecute(parameter)) return;
    await handler.execute(parameter);
    this.listeners.forEach(cb => cb(commandId, parameter));
  }

  onCommandInvoked(callback) {
    this.listeners.push(callback);
    return () => {
      const i = this.listeners.indexOf(callback);
      if (i >= 0) this.listeners.splice(i, 1);
    };
  }
}