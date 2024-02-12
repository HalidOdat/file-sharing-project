import { browser } from "$app/environment";

export const getToken = (): string | null => {
    if (browser) {
        const token = sessionStorage.getItem("token");
        return token;
    }

    return null
}