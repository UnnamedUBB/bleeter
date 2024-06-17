// HomeContent.tsx
import React, { useState } from 'react';
import { Avatar, AvatarImage, AvatarFallback } from "@/components/ui/avatarpost";
import { Card, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { Link } from "react-router-dom";
import axiosInstance from './axiosConfig'; // Import the configured Axios instance
import AddBleetForm from './AddBleetForm';
import BleetList from './BleetList';
function HomeContent() {
    const [bleetText, setBleetText] = useState('');
    const [error, setError] = useState<string | null>(null);

    const handlePostBleet = async (e: React.FormEvent) => {
        e.preventDefault();
        setError(null);

        try {
            const response = await axiosInstance.post('/bleet', {
                content: bleetText,
            });

            console.log('Bleet posted:', response.data);

            if (response.status === 201) {
                setBleetText(''); // Clear the input after successful post
            } else {
                setError('Failed to post bleet');
            }
        } catch (error: any) {
            console.error('Error posting bleet:', error);
            if (error.response) {
                setError('Failed to post bleet: ' + (error.response.data?.message || error.response.statusText));
            } else if (error.request) {
                setError('No response from server. Please check your network or server status.');
            } else {
                setError('Error: ' + error.message);
            }
        }
    };

    return (
        <>
            <div className="w-2/3 mx-auto">
                <Card className="p-4 shadow-lg rounded-lg border border-green-950">
                    <CardHeader>
                        <CardTitle className="flex items-center space-x-4">
                            <Avatar>
                                <AvatarImage src="https://github.com/shadcn.png" />
                                <AvatarFallback>CN</AvatarFallback>
                            </Avatar>
                            <input
                                type="text"
                                placeholder="Aa"
                                className="input bg-slate-900 text-white rounded-xl p-3 h-10 w-full max-w-lg"
                                value={bleetText}
                                onChange={(e) => setBleetText(e.target.value)}
                            />
                            <svg
                                xmlns="http://www.w3.org/2000/svg"
                                width="16"
                                height="16"
                                fill="currentColor"
                                className="bi bi-image fill-green-600 h-8 w-8 cursor-pointer"
                                viewBox="0 0 16 16"
                            >
                                <path d="M6.002 5.5a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0" />
                                <path d="M2.002 1a2 2 0 0 0-2 2v10a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V3a2 2 0 0 0-2-2zm12 1a1 1 0 0 1 1 1v6.5l-3.777-1.947a.5.5 0 0 0-.577.093l-3.71 3.71-2.66-1.772a.5.5 0 0 0-.63.062L1.002 12V3a1 1 0 0 1 1-1z" />
                            </svg>
                            <button
                                className="btn bg-green-600 hover:bg-green-700 text-white font-semibold rounded-xl w-20 h-10"
                                onClick={handlePostBleet}
                            >
                                Post
                            </button>
                        </CardTitle>
                    </CardHeader>
                </Card>
                {error && <p className="text-red-500 mt-4">{error}</p>}
            </div>
            <div>
                <AddBleetForm />
            </div>
            <div>
                <BleetList />
            </div>
            
        </>
    );
}

export default HomeContent
