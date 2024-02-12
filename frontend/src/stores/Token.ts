import { browser } from "$app/environment";
import { writable } from "svelte/store";

export const getToken = (): string | null => {
    if (browser) {
        const token = sessionStorage.getItem("token");
        return token;
    }

    return null
}

export const email = writable(((): string | null => {
    if (browser) {
        return sessionStorage.getItem("email")
    }
    return null;
})())
