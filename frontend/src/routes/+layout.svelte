<script>
  import "../app.css";
  import { AppShell } from "@skeletonlabs/skeleton";
  import AppBar from "../lib/AppBar.svelte";
  import { Toast } from "@skeletonlabs/skeleton";
  import { initializeStores } from "@skeletonlabs/skeleton";

  initializeStores();
</script>

<Toast />

<AppShell slotPageContent="h-56 grid gap-2 justify-items-center p-4">
  <svelte:fragment slot="header">
    <AppBar />
  </svelte:fragment>
  <!-- (sidebarLeft) -->
  <!-- (sidebarRight) -->
  <!-- (pageHeader) -->
  <!-- Router Slot -->
  <slot />
  <!-- ---- / ---- -->
  <!-- (pageFooter) -->
  <!-- (footer) -->
</AppShell>
