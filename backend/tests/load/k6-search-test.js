import http from 'k6/http';
import { sleep, check } from 'k6';

export const options = {
    vus: 10,
    duration: '30s',
};

export default function () {
    const url = 'http://localhost:5000/api/restaurant/search?lat=-23.55&lng=-46.63&radius=10';
    const res = http.get(url);

    check(res, {
        'status is 200': (r) => r.status === 200,
        'latency is low': (r) => r.timings.duration < 200,
    });

    sleep(1);
}
