import { writable } from 'svelte/store';

interface ObjectState {
    onClick?: () => void,
}

export default writable({} as ObjectState);
