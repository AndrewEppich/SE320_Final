const API_BASE_URL = 'https://localhost:7096'; // Try HTTP first

export const api = {
    async getReceipts(sortBy = null, sortOrder = 'asc') {
        try {
            const queryParams = new URLSearchParams();
            if (sortBy) {
                queryParams.append('sortBy', sortBy);
                queryParams.append('sortOrder', sortOrder);
            }
            
            console.log('Attempting to fetch from:', `${API_BASE_URL}/api/Receipts?${queryParams.toString()}`);
            const response = await fetch(`${API_BASE_URL}/api/Receipts?${queryParams.toString()}`, {
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