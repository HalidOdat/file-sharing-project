<script lang="ts">
  import Icon from "svelte-awesome";
  import send from "svelte-awesome/icons/send";
  import download from "svelte-awesome/icons/download";

  import { AppBar } from "@skeletonlabs/skeleton";
  import { Avatar } from "@skeletonlabs/skeleton";

  import AppBarState from "../stores/AppBarState";
</script>

<AppBar gridColumns="grid-cols-3" slotDefault="place-self-center" slotTrail="place-content-end">
  <svelte:fragment slot="lead"><Icon data={send} scale={1.5} /></svelte:fragment>

  {#if $AppBarState.onClick}
    <button type="button" class="btn variant-filled p-3 px-9" on:click={$AppBarState.onClick}>
      <Icon data={download} scale={1.5} />
      <span class="font-bold font-mono">Download</span>
    </button>
  {:else}
    <span class="font-bold text-lg">File Sharing</span>
  {/if}

  <svelte:fragment slot="trail">
    <Avatar src="https://i.pravatar.cc/" width="w-12" />
  </svelte:fragment>
</AppBar>
