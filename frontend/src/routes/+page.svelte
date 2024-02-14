<script lang="ts">
  import { type AxiosProgressEvent } from "axios";
  import FileDrop from "$lib/FileDrop.svelte";
  import { ProgressBar } from "@skeletonlabs/skeleton";
  import Display from "$lib/Display.svelte";
  import clipboard from "svelte-awesome/icons/clipboard";
  import AppBarState from "../stores/AppBarState";
  import { getToken } from "../stores/Token";
  import { getToastStore } from "@skeletonlabs/skeleton";
  import upload from "svelte-awesome/icons/upload";
  import { goto } from "$app/navigation";
  import { browser } from "$app/environment";
  import { api } from "../Server";

  const toastStore = getToastStore();

  let token = getToken();
  if (token === null && browser) {
    goto("/login");
  }

  $AppBarState.onClick = undefined;

  let files: FileList;
  let progress = 0;
  let fileUrl = "";
  let promise: Promise<string> | null = null;
  let preview: Promise<{ content: string; contentType: string }> | null = null;

  const onChangeHandler = () => {
    let file = files.item(0);
    preview = getPreviewData(file);

    $AppBarState.icon = { data: upload };
    $AppBarState.text = "Upload";
    $AppBarState.onClick = () => {
      promise = sendFile(file);
    };
  };

  const blobToBase64 = (blob: Blob): Promise<string> => {
    return new Promise((resolve, _) => {
      const reader = new FileReader();
      reader.onloadend = () => resolve(reader.result as string);
      reader.readAsDataURL(blob);
    });
  };

  const getPreviewData = async (file: File): Promise<{ content: string; contentType: string }> => {
    let content = await blobToBase64(file);
    return {
      content: content.substring(`data:${file.type};base64,`.length),
      contentType: file.type
    };
  };

  const sendFile = async (file: File): Promise<string> => {
    let formData = new FormData();
    formData.append("fileName", file.name);
    formData.append("formFile", file);

    let response = await api.request({
      method: "post",
      url: "/v1/api/file/upload",
      data: formData,
      headers: {
        Authorization: `Bearer ${token}`
      },
      onUploadProgress: (p: AxiosProgressEvent) => {
        console.log(p);
        progress = Number(p.progress) * 100;
      }
    });
    const id = response.data;

    console.log(id);

    fileUrl = `${window.location.origin}/${id}`;

    $AppBarState.onClick = () => {
      navigator.clipboard.writeText(fileUrl);
      toastStore.trigger({ message: `Copied to clipboard!`, timeout: 800 });
    };
    $AppBarState.icon = { data: clipboard };
    $AppBarState.text = "Copy URL";

    toastStore.trigger({ message: `Created new file with id (${id})`, timeout: 2000 });

    return id;
  };
</script>

{#if !preview}
  <FileDrop bind:files onChange={onChangeHandler} />
{:else}
  {#if promise}
    {#await promise}
      <ProgressBar label="Progress Bar" value={progress} max={100} />
    {:then response}
      <div class="w-full text-token card variant-soft p-4 flex items-center gap-4">
        <input
          class="input text-2xl font-mono p-4 text-center"
          type="text"
          readonly
          value={fileUrl}
        />
      </div>
    {:catch error}
      <h1 class="text-error-800 text-center">Error: {error.message}</h1>
    {/await}
  {/if}

  {#await preview}
    <section class="card w-full">
      <div class="p-4 space-y-4 px-20">
        {#each Array(2) as _, index (index)}
          <div class="placeholder animate-pulse" />
          <div class="grid grid-cols-3 gap-8">
            <div class="placeholder animate-pulse" />
            <div class="placeholder animate-pulse" />
            <div class="placeholder animate-pulse" />
          </div>
          <div class="grid grid-cols-4 gap-4">
            <div class="placeholder animate-pulse" />
            <div class="placeholder animate-pulse" />
            <div class="placeholder animate-pulse" />
            <div class="placeholder animate-pulse" />
          </div>
        {/each}
      </div>
    </section>
  {:then resolvedData}
    <Display data={resolvedData} />
  {:catch error}
    <h1 class="text-error-800 text-center">Error: {error.message}</h1>
  {/await}
{/if}
