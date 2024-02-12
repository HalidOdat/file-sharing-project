<script lang="ts">
  import spinner from "svelte-awesome/icons/spinner";
  import AppBarState from "../../stores/AppBarState";
  import { goto } from "$app/navigation";
  import userCircleO from "svelte-awesome/icons/userCircleO";
  import { api } from "../../Server";
  import { email } from "../../stores/Token";

  let emailValue = "";
  let passwordValue = "";

  $AppBarState.onClick = async () => {
    $AppBarState.icon = { data: spinner, pulse: true };

    let response = await api.postForm("/api/v1/authenticate/login", {
      email: emailValue,
      password: passwordValue
    });

    if (response.status != 200) {
      throw 10;
    }

    sessionStorage.setItem("token", response.data.token);
    sessionStorage.setItem("email", emailValue);
    $email = emailValue;

    console.log(response);

    goto("/");
  };
  $AppBarState.icon = { data: userCircleO };
  $AppBarState.text = "Login!";
</script>

<form method="post" class="p-2">
  <div class="input-group input-group-divider grid-cols-[1fr_auto] my-2">
    <input
      class="input variant-form-material text-xl p-4"
      title="Input (email)"
      bind:value={emailValue}
      type="email"
      placeholder="john@example.com"
      autocomplete="email"
    />
    <!-- <a href="#" title="Username already in use.">(icon)</a> -->
  </div>

  <div class="input-group input-group-divider grid-cols-[1fr_auto] my-2">
    <input
      class="input variant-form-material text-xl p-4"
      bind:value={passwordValue}
      title="Input (password)"
      type="password"
      placeholder="Enter Password..."
    />
    <!-- <a href="/" title="Username already in use.">(icon)</a> -->
  </div>
  <span class="text-amber-200">
    If you don't have an account <a class="text-primary-400" href="/register">register here</a>!
  </span>
</form>
