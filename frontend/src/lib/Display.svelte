<script lang="ts">
  import { HighlightAuto, LineNumbers } from "svelte-highlight";
  import atomOneDark from "svelte-highlight/styles/atom-one-dark";

  export let data: { content: string; contentType: string };
</script>

<svelte:head>
  {@html atomOneDark}
</svelte:head>

{#if data.contentType.startsWith("text") || data.contentType === "application/json"}
  <HighlightAuto code={atob(data.content)} let:highlighted>
    <LineNumbers {highlighted} />
  </HighlightAuto>
{:else if data.contentType.startsWith("image")}
  <img src={`data:${data.contentType};base64,${data.content}`} alt="" srcset="" />
{:else}
  <!-- <h2 class="font-bold text-3xl text-center">Can't render the file ({data.contentType})...</h2> -->
{/if}
