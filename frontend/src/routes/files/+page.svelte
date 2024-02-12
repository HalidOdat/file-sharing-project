<script lang="ts">
  import axios, { type AxiosProgressEvent } from "axios";
  import AppBarState from "../../stores/AppBarState";
  import { getToken } from "../../stores/Token";
  import { Icon } from "svelte-awesome";
  import trash from "svelte-awesome/icons/trash";

  let token = getToken();

  $AppBarState.onClick = undefined;

  const location = `https://localhost:5001/v1/api/file/getAll`;
  const getFileList = async (): Promise<object[]> => {
    let response = await axios.request({
      method: "post",
      url: location,
      headers: {
        Authorization: `Bearer ${token}`
      }
    });

    if (response.status != 200) {
      throw 10;
    }

    console.log(response);

    return response.data;
  };

  let promise: Promise<object[]> = getFileList();

  const deleteItem = async (id: string) => {
    let formData = new FormData();
    formData.append("id", id);
    const location = `https://localhost:5001/v1/api/file/delete`;
    let response = await axios.request({
      method: "delete",
      url: location,
      data: formData,
      headers: {
        Authorization: `Bearer ${token}`
      }
    });
    console.log(id);

    if (response.status != 200) {
      throw 10;
    }

    let value = await promise;
    promise = Promise.resolve(value.filter((file) => file.id != id));
  };

  const toSizing = (n: number): string => {
    if (n < 1024) {
      return `${n} B`;
    } else if (n < 1024 * 1024) {
      return `${(n / 1024).toFixed(2)} KB`;
    } else {
      return `${(n / (1024 * 1024)).toFixed(2)} KB`;
    }
  };
</script>

<!-- {#each Array(1) as _, index (index)}{/each} -->

{#await promise}
  <section class="card w-full">
    <div class="p-4 space-y-4 px-20">
      {#each Array(1) as _, index (index)}
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
{:then data}
  <div class="table-container">
    <!-- Native Table Element -->
    <table class="table table-hover">
      <thead>
        <tr>
          <th>Name</th>
          <th>Content Type</th>
          <th>Size</th>
          <th class="text-center">Action</th>
        </tr>
      </thead>
      <tbody>
        {#each data as row, i}
          <tr>
            <td>{row.name}</td>
            <td>{row.contentType}</td>
            <td>{toSizing(row.size)}</td>
            <td class="text-center">
              <button
                type="button"
                class="btn variant-filled-error text-m"
                on:click={() => deleteItem(row.id)}
              >
                <Icon data={trash} />
                <span>Button</span>
              </button>
            </td>
          </tr>
        {/each}
      </tbody>
      <tfoot>
        <tr>
          <th colspan="2">Total</th>
          <td>{toSizing(data.reduce((acc, row) => row.size + acc, 0))}</td>
          <td class="text-center">{data.length}</td>
        </tr>
      </tfoot>
    </table>
  </div>
{:catch error}
  <!-- <p style="color: red">Cou{error.message}</p> -->
{/await}
