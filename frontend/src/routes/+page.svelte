<script lang="ts">
  import App from "../lib/App.svelte";
  import AppBarState from "../stores/AppBarState";

  $AppBarState.onClick = undefined;
</script>

<App />

<style lang="postcss">
  :global(html) {
    background-color: theme(colors.gray.100);
  }
</style>
