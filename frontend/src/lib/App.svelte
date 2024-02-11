<script lang="ts">
  import axios, { type AxiosProgressEvent } from "axios";
  import FileDrop from "./FileDrop.svelte";
  import { ProgressBar } from "@skeletonlabs/skeleton";
  import Display from "./Display.svelte";
  import clipboard from "svelte-awesome/icons/clipboard";
  import AppBarState from "../stores/AppBarState";
  import { getToastStore } from "@skeletonlabs/skeleton";

  const toastStore = getToastStore();

  $AppBarState.onClick = undefined;

  let uploaded = false;
  let files: FileList;
  let promise: Promise<string> = Promise.resolve("Empty");
  let progress = 0;
  let fileUrl = "";
  let data = {};

  const location = `https://localhost:5001/v1/api/file/upload`;

  const onChangeHandler = (e: Event): void => {
    // console.log('file data:', e, files, e.target.files[0]);

    let file = files.item(0);
    promise = sendFile(file);
    uploaded = true;
  };

  const sendFile = async (file: File): Promise<string> => {
    let formData = new FormData();
    formData.append("fileName", file.name);
    formData.append("formFile", file);
    let response = await axios.request({
      method: "post",
      url: location,
      data: formData,
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
    $AppBarState.icon = clipboard;
    $AppBarState.text = "Copy URL";

    toastStore.trigger({ message: `Created new file with id (${id})`, timeout: 2000 });

    function blobToBase64(blob: Blob): Promise<string> {
      return new Promise((resolve, _) => {
        const reader = new FileReader();
        reader.onloadend = () => resolve(reader.result as string);
        reader.readAsDataURL(blob);
      });
    }
    let content = await blobToBase64(file);
    data = {
      content: content.substring(`data:${file.type};base64,`.length),
      contentType: file.type
    };
    console.log(data);

    return id;
  };
</script>

{#if !uploaded}
  <FileDrop bind:files onChange={onChangeHandler} />
{:else}
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
    <Display {data} />
  {:catch error}
    <p style="color: red">{error.message}</p>
  {/await}
{/if}
