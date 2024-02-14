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
    <LineNumbers {highlighted} wrapLines={true} />
  </HighlightAuto>
{:else if data.contentType.startsWith("image")}
  <img src={`data:${data.contentType};base64,${data.content}`} alt="" srcset="" />
{:else if data.contentType.startsWith("video") || data.contentType.startsWith("audio")}
  <!-- svelte-ignore a11y-media-has-caption -->
  <video class="" controls>
    <source src={`data:${data.contentType};base64,${data.content}`} type={data.contentType} />
    Your browser does not support the video tag.
  </video>
{:else}
  <h2 class="font-bold text-3xl text-center">
    Can't render the file (of type {data.contentType})...
  </h2>
{/if}
