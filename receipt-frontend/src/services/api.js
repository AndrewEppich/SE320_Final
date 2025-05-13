const API_BASE_URL = 'https://localhost:7096'; // Try HTTP first

export const api = {
    async getReceipts() {
        try {
            console.log('Attempting to fetch from:', `${API_BASE_URL}/api/Receipts`);
            const response = await fetch(`${API_BASE_URL}/api/Receipts`, {
                method: 'GET',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
                credentials: 'include'
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const data = await response.json();
            console.log('Received data:', data);
            return data;
        } catch (error) {
            console.error('Error fetching receipts:', error);
            throw error;
        }
    }
};