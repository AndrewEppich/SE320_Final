const API_BASE_URL = 'http://localhost:5000';

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
    },

    async getWeeklySummary(year, month, week) {
        try {
            const response = await fetch(
                `${API_BASE_URL}/api/Summary/weekly?year=${year}&month=${month}&week=${week}`,
                {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                    },
                    credentials: 'include'
                }
            );

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            return await response.json();
        } catch (error) {
            console.error('Error fetching weekly summary:', error);
            throw error;
        }
    },

    async getMonthlySummary(year, month) {
        try {
            const response = await fetch(
                `${API_BASE_URL}/api/Summary/monthly?year=${year}&month=${month}`,
                {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                    },
                    credentials: 'include'
                }
            );

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            return await response.json();
        } catch (error) {
            console.error('Error fetching monthly summary:', error);
            throw error;
        }
    },

    async getVendorSummary(vendor) {
        try {
            const response = await fetch(
                `${API_BASE_URL}/api/Summary/vendor?name=${encodeURIComponent(vendor)}`,
                {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                    },
                    credentials: 'include'
                }
            );

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            return await response.json();
        } catch (error) {
            console.error('Error fetching vendor summary:', error);
            throw error;
        }
    },

    async getAllVendors() {
        try {
            const response = await fetch(
                `${API_BASE_URL}/api/Summary/vendors`,
                {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                    },
                    credentials: 'include'
                }
            );

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const data = await response.json();
            console.log('Received data:', data);
            return data;
        } catch (error) {
            console.error('Error fetching vendors:', error);
            throw error;
        }
    }
};