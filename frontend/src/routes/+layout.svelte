<script>
  import "../app.css";

  import { AppShell } from '@skeletonlabs/skeleton';

  import AppBar from '../lib/AppBar.svelte';
</script>

<AppShell slotPageContent="h-56 grid gap-4 content-around p-4">
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

