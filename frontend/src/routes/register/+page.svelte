<script lang="ts">
  import spinner from "svelte-awesome/icons/spinner";
  import AppBarState from "../../stores/AppBarState";
  import { book } from "svelte-awesome/icons";
  import { goto } from "$app/navigation";
  import { api } from "../../Server";

  let email = "";
  let password = "";

  $AppBarState.onClick = async () => {
    $AppBarState.icon = { data: spinner, pulse: true };

    let response = await api.postForm("/api/v1/authenticate/register", {
      email,
      password
    });

    if (response.status != 200) {
      throw 10;
    }

    sessionStorage.setItem("token", response.data.token);

    console.log(response);

    goto("/");
  };
  $AppBarState.icon = { data: book };
  $AppBarState.text = "Register!";
</script>

<form method="post" class="p-2">
  <div class="input-group input-group-divider grid-cols-[1fr_auto] my-2">
    <input
      class="input variant-form-material text-xl p-4"
      title="Input (email)"
      bind:value={email}
      type="email"
      placeholder="john@example.com"
      autocomplete="email"
    />
    <!-- <a href="#" title="Username already in use.">(icon)</a> -->
  </div>

  <div class="input-group input-group-divider grid-cols-[1fr_auto] my-2">
    <input
      class="input variant-form-material text-xl p-4"
      bind:value={password}
      title="Input (password)"
      type="password"
      placeholder="Enter Password..."
    />
    <!-- <a href="/" title="Username already in use.">(icon)</a> -->
  </div>
  <span class="text-amber-200">
    If you already have an account <a class="text-primary-400" href="/login">login here</a>!
  </span>
</form>
