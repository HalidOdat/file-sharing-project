<script lang="ts">
  import axios, { type AxiosProgressEvent } from "axios";
  import { ProgressBar } from "@skeletonlabs/skeleton";
  import atomOneDark from "svelte-highlight/styles/atom-one-dark";
  import AppBarState from "../../stores/AppBarState.js";
  import Display from "$lib/Display.svelte";
  import download from "svelte-awesome/icons/download";

  export let data;

  const location = `https://localhost:5001/v1/api/file/download`;

  let progress = 0;
  const getFile = async (id: string): Promise<object> => {
    let response = await axios.request({
      method: "get",
      url: `${location}?id=${id}`,
      onUploadProgress: (p: AxiosProgressEvent) => {
        console.log(p);
        progress = Number(p.progress) * 100;
      }
    });
    console.log(response);

    $AppBarState.onClick = () => {
      var link = document.createElement("a");
      link.href = `data:${response.data.contentType};base64,${response.data.content}`;
      link.download = response.data.name;
      link.click();
    };
    $AppBarState.icon = download;
    $AppBarState.text = "Download";
    return response;
  };

  let promise = getFile(data.slug);
</script>

<svelte:head>
  {@html atomOneDark}
</svelte:head>

{#await promise}
  <ProgressBar label="Progress Bar" value={progress} max={100} />
{:then response}
  <Display data={{ content: response.data.content, contentType: response.data.contentType }} />
{:catch error}
  <p style="color: red">{error.message}</p>
{/await}
