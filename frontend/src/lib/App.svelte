<script lang="ts">
  import axios, {isCancel, AxiosError} from 'axios';
  import type {AxiosProgressEvent} from 'axios';
  import FileDrop from './FileDrop.svelte'
  import { ProgressBar } from '@skeletonlabs/skeleton';

  let uploaded = false;
  let files: FileList;
  let promise: Promise<string> = Promise.resolve("Empty")
  let progress = 0
  let fileUrl = ""

  const location = `https://localhost:5001/v1/api/file/upload`

  const onChangeHandler = (e: Event): void => {
    // console.log('file data:', e, files, e.target.files[0]);

    let file = files.item(0);
    promise = sendFile(file)
    uploaded = true
  }

  const onCopy = () => {
    navigator.clipboard.writeText(fileUrl)
  }

  const sendFile = async (file: File): Promise<string> => {
    let formData = new FormData();
    formData.append('fileName', file.name);
    formData.append('formFile', file);
    let response = await axios.request({
        method: "post",
        url: location,
        data: formData,
        onUploadProgress: (p: AxiosProgressEvent) => {
          console.log(p);
          progress = Number(p.progress) * 100
        }
    });
    const content = response.data

    console.log(content);

    fileUrl = `${window.location.origin}/${content}`
    return content
  }
</script>

{#if !uploaded}
  <FileDrop bind:files={files} onChange={onChangeHandler} />
{:else}
  {#await promise}
    <ProgressBar label="Progress Bar" value={progress} max={100} />
  {:then response}
    <div class="w-full text-token card variant-soft p-4 flex items-center gap-4">
      <input class="input text-xl" type="text" readonly value={fileUrl} />
      <button on:click={onCopy} class="btn variant-filled text-xl">Copy</button>
    </div>
  {:catch error}
    <p style="color: red">{error.message}</p>
  {/await}
{/if}



