<script lang="ts">
  import Icon from "svelte-awesome";
  import file from "svelte-awesome/icons/file";

  import { FileDropzone } from "@skeletonlabs/skeleton";

  export let files: FileList;
  export let onChange: (e: Event) => void;
</script>

<FileDropzone name="files" bind:files on:change={onChange} padding="p-8 py-32">
  <svelte:fragment slot="lead"><Icon data={file} scale={4.5} /></svelte:fragment>
  <svelte:fragment slot="message">
    <span class="font-bold">Upload a file</span> or drag and drop
  </svelte:fragment>
  <svelte:fragment slot="meta">PNG, TXT, PDF, etc</svelte:fragment>
</FileDropzone>
