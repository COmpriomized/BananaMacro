export function renderRibbon(manager, container) {
  container.innerHTML = "";
  const groups = manager.getGroups();

  groups.forEach(group => {
    const groupEl = document.createElement("div");
    groupEl.className = "ribbon-group";

    const title = document.createElement("div");
    title.className = "ribbon-group-title";
    title.textContent = group.title;
    groupEl.appendChild(title);

    const itemsEl = document.createElement("div");
    itemsEl.className = "ribbon-items";

    group.items
      .filter(i => i.visible)
      .sort((a, b) => (a.order || 0) - (b.order || 0))
      .forEach(item => {
        const btn = document.createElement("button");
        btn.className = "ribbon-button";
        btn.textContent = item.label;
        btn.title = item.tooltip || "";
        btn.disabled = !item.enabled;
        btn.onclick = () => manager.invokeCommand(item.commandId);
        itemsEl.appendChild(btn);
      });

    groupEl.appendChild(itemsEl);
    container.appendChild(groupEl);
  });
}