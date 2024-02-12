import type { IconProps } from 'svelte-awesome/components/Icon.svelte';
import { writable } from 'svelte/store';

interface ObjectState {
    onClick?: () => void,
    text: string,
    icon: IconProps,
}

export default writable({} as ObjectState);
