# Navigation and Context

## Workspace Navigation

| Rule | Description |
|------|-------------|
| Workspace switch preserves Context | Selection Context is maintained across workspace boundaries |
| Back navigation restores state | Previous workspace's Store state is preserved |
| Navigation is explicit | User must click sidebar or use keyboard shortcut |
| No auto-navigation | System never navigates without user intent |

## Context Propagation

When a user navigates between workspaces, Selection Context follows:

```text
Cashier: Table 8 selected ──→ Context: { type: "table", id: "8" }
                                  │
                                  ▼
Kitchen: receives context → filter tickets for Table 8
   │                           Context Bar: "Table 8"
   │
   ▼
Dashboard: receives context → show metrics for Table 8
   │                           Context Bar: "Table 8"
   │
   ▼
Cashier (back): Table 8 still selected → Order Panel restored
```

## Workspace Activation/Deactivation

```text
Navigate to workspace → Store.Initialize() → fetch data → render
                                                              │
Navigate away → Store state preserved (not disposed)
                                                              │
Navigate back → Store state restored → no refetch needed
                                                              │
Long idle → Store disposed → refetch on next activation
```

## Selection Persistence

| Selection | Persists Across Workspaces? | Cleared By |
|-----------|---------------------------|------------|
| Table | Yes | Escape, new selection |
| Order | Yes | Escape, new selection |
| Kitchen Ticket | Yes | Escape, new selection |
| Product | No (modal scope) | Modal close |

## Cross-workspace Synchronization

When one workspace modifies shared data, other workspaces must reflect the change:

| Event | Affected Workspaces |
|-------|-------------------|
| Order Confirmed | Cashier (state change), Kitchen (new ticket), Dashboard (metrics) |
| Payment Received | Cashier (table released), Dashboard (sales) |
| Ticket Ready | Kitchen (state change), Cashier (badge), Dashboard (metrics) |

Synchronization happens via polling and will be replaced by SignalR in future.