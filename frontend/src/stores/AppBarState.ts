import type { IconType } from 'svelte-awesome/components/Icon.svelte';
import { writable } from 'svelte/store';

interface ObjectState {
    onClick?: () => void,
    text: string,
    icon: IconType,
}

export default writable({} as ObjectState);
