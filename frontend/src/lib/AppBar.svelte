<script lang="ts">
  import Icon from 'svelte-awesome'
  import send from 'svelte-awesome/icons/send';

  import { AppBar } from '@skeletonlabs/skeleton';
  import { Avatar } from '@skeletonlabs/skeleton';
</script>

<AppBar gridColumns="grid-cols-3" slotDefault="place-self-center" slotTrail="place-content-end">
	<svelte:fragment slot="lead"><Icon data={send} scale=1.5/></svelte:fragment>
	<span class="font-bold text-lg">File Sharing</span>

	<svelte:fragment slot="trail">
    <Avatar src="https://i.pravatar.cc/" width="w-12" />
  </svelte:fragment>
</AppBar>

