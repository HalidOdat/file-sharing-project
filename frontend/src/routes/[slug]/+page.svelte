<script lang="ts">
  import axios from "axios";
  import type { AxiosProgressEvent } from "axios";
  import { ProgressBar } from "@skeletonlabs/skeleton";
  import { HighlightAuto, LineNumbers } from "svelte-highlight";
  import atomOneDark from "svelte-highlight/styles/atom-one-dark";
  import AppBarState from "../../stores/AppBarState.js";

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
  <HighlightAuto code={atob(response.data.content)} let:highlighted>
    <LineNumbers {highlighted} />
  </HighlightAuto>
{:catch error}
  <p style="color: red">{error.message}</p>
{/await}
