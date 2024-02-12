import { browser } from "$app/environment";
import { goto } from "$app/navigation";

export const getToken = (): string | null => {
    if (browser) {
        const token = sessionStorage.getItem("token");
        if (token === null) {
            goto("/login");
        }

        return token;
    }

    return null
}