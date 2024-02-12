<script lang="ts">
  import AppBarState from "../../stores/AppBarState";
  import { email, getToken } from "../../stores/Token";
  import { Icon } from "svelte-awesome";
  import trash from "svelte-awesome/icons/trash";
  import { goto } from "$app/navigation";
  import { browser } from "$app/environment";
  import leaf from "svelte-awesome/icons/leaf";
  import { api } from "../../Server";
  import { getToastStore } from "@skeletonlabs/skeleton";
  import clipboard from "svelte-awesome/icons/clipboard";

  const toastStore = getToastStore();

  let token = getToken();
  if (token === null && browser) {
    goto("/login");
  }

  $AppBarState.onClick = async () => {
    await api.request({
      method: "post",
      url: "/v1/api/file/getAll",
      headers: {
        Authorization: `Bearer ${token}`
      }
    });

    if (browser) {
      sessionStorage.removeItem("token");
      sessionStorage.removeItem("email");
      $email = null;
      window.location = "/";
    }
  };
  $AppBarState.icon = { data: leaf };
  $AppBarState.text = "LogOut";

  const getFileList = async (): Promise<object[]> => {
    let response = await api.request({
      method: "post",
      url: "/v1/api/file/getAll",
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
    let response = await api.request({
      method: "delete",
      url: "/v1/api/file/delete",
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

{#await promise}
  <section class="card w-full">
    <div class="p-4 space-y-4 px-20">
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
    </div>
  </section>
{:then data}
  <div class="table-container">
    <!-- Native Table Element -->
    <table class="table table-hover">
      <thead>
        <tr>
          <th>ID</th>
          <th>Name</th>
          <th>Content Type</th>
          <th>Size</th>
          <th class="text-center">Action</th>
        </tr>
      </thead>
      <tbody>
        {#each data as row, i}
          <tr>
            <td>{row.id}</td>
            <td>{row.name}</td>
            <td>{row.contentType}</td>
            <td>{toSizing(row.size)}</td>
            <td class="text-center">
              <button
                type="button"
                class="btn variant-filled-success text-m"
                on:click={() => {
                  navigator.clipboard.writeText(`${window.location.origin}/${row.id}`);
                  toastStore.trigger({ message: `Copied to clipboard!`, timeout: 800 });
                }}
              >
                <Icon data={clipboard} />
                <span>Copy URL</span>
              </button>

              <button
                type="button"
                class="btn variant-filled-error text-m"
                on:click={() => deleteItem(row.id)}
              >
                <Icon data={trash} />
                <span>Delete</span>
              </button>
            </td>
          </tr>
        {/each}
      </tbody>
      <tfoot>
        <tr>
          <th colspan="3">Total</th>
          <td>{toSizing(data.reduce((acc, row) => row.size + acc, 0))}</td>
          <td class="text-center">{data.length}</td>
        </tr>
      </tfoot>
    </table>
  </div>
{:catch error}
  <!-- <p style="color: red">Cou{error.message}</p> -->
{/await}
