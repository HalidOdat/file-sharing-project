<script lang="ts">
  import Icon from "svelte-awesome";
  import send from "svelte-awesome/icons/send";
  import { AppBar } from "@skeletonlabs/skeleton";
  import { Avatar } from "@skeletonlabs/skeleton";
  import AppBarState from "../stores/AppBarState";
  import plus from "svelte-awesome/icons/plus";
  import { goto } from "$app/navigation";

  const goToUpload = () => {
    window.location = "/";
  };
</script>

<AppBar gridColumns="grid-cols-3" slotDefault="place-self-center" slotTrail="place-content-end">
  <svelte:fragment slot="lead"><Icon data={send} scale={1.5} /></svelte:fragment>

  {#if $AppBarState.onClick}
    <button type="button" class="btn variant-filled p-3 px-9" on:click={$AppBarState.onClick}>
      <Icon {...$AppBarState.icon} scale={1.5} />
      <span class="font-bold font-mono">{$AppBarState.text}</span>
    </button>
  {:else}
    <span class="font-bold text-lg">File Sharing</span>
  {/if}

  <svelte:fragment slot="trail">
    <button type="button" class="btn variant-filled p-3 px-9" on:click={goToUpload}>
      <Icon data={plus} scale={1.5} />
      <span class="font-bold font-mono">New </span>
    </button>
    <Avatar initials="JD" width="w-12" on:click={() => goto("/files")} />
  </svelte:fragment>
</AppBar>
