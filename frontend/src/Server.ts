import { env } from '$env/dynamic/public';
import axios from 'axios';

export const severURL = env.PUBLIC_SERVER_URL ?? "http://localhost:5000"

export const api = axios.create({ baseURL: severURL })

