import {Button, Col, PageItem, Pagination, Row, Table} from "react-bootstrap";
import {CellValue, Column, usePagination, useSortBy, useTable} from 'react-table';
import {JSXElementConstructor, Key, ReactElement, ReactFragment, useMemo} from "react";
import {useVehicleStore} from "../store/vehicleStore";
import {VehicleDto, VehicleTableDto} from "../models/vehicles.type";
import {deleteVehicles} from "../services/vehicle.services";
import {useNavigate} from "react-router";
import VehicleCrudModal from "./VehicleCrudModal";
import useErrorHandler from "../../../shared/hooks/useErrorHandler";

interface Props {
    vehicles: VehicleTableDto[];
}

const VehicleTable = (): JSX.Element => {

    const {remove, setShowModal, setSelectedVehicle, clearSelectedVehicle} = useVehicleStore();
    const vehicles = useVehicleStore(state => state.vehicles);
    const {handleError} = useErrorHandler();

    function handleDelete(value: CellValue<string> | CellValue<number> | CellValue<any>) {
        deleteVehicles(value).then(dto => {
            remove(dto.id);
        }).catch(reason => {
            handleError(reason);
        });
    }

    function handleEdit(value: CellValue<string> | CellValue<number> | CellValue<any>) {
        const id = Number(value);
        setSelectedVehicle(id);
        setShowModal(true);
    }

    const columns = useMemo<Column<VehicleTableDto>[]>(
        () => [
            {
                Header: 'ID',
                accessor: 'id',
            },
            {
                Header: 'Tipo de vehículo',
                accessor: 'vehicleType',
            },
            {
                Header: 'Número de ruedas',
                accessor: 'wheels',
            },
            {
                Header: 'Marca',
                accessor: 'brand',
            },
            {
                Header: 'Modelo',
                accessor: 'model',
            },
            {
                Header: 'Patente',
                accessor: 'patent',
            },
            {
                Header: 'Número de chasis',
                accessor: 'chassisNumber',
            },
            {
                Header: 'Kilómetros',
                accessor: 'kilometers',
            }, {
                Header: 'Acciones',
                id: "idAccions",
                accessor: 'id',
                Cell: ({value}) => (
                    <>
                        <Button variant="primary" onClick={() => handleEdit(value)}>Editar</Button>
                        <Button variant="danger" onClick={() => handleDelete(value)}>Borrar</Button>
                    </>
                ),
            },
        ],
        []
    );

    let tableInstance: any = useTable({
        columns,
        data: vehicles,

    }, useSortBy, usePagination);
    tableInstance = {...tableInstance, initialState: {pageSize: 10},}
    const {
        getTableProps, getTableBodyProps, headerGroups, rows, prepareRow,
        canPreviousPage,
        canNextPage,
        page,
        pageOptions,
        pageCount,
        gotoPage,
        nextPage,
        previousPage,
        setPageSize,
        state: {pageIndex, pageSize},
    } = tableInstance;

    const onClickHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        clearSelectedVehicle();
        setShowModal(true);
    };
    return (
        <>
            <Row>
                <Col xl={2} xxl={2} md={2} sm={12}>
                    <Button variant={"success"} id={"create-vehicle"} onClick={onClickHandler}>Nuevo</Button>
                </Col>
            </Row>
            <Row>
                <Col>
                    <Table bordered={true} hover responsive={true}>
                        <thead className={"thead-dark"}>
                        {headerGroups.map((headerGroup: any) => (
                            <tr {...headerGroup.getHeaderGroupProps()}>
                                {headerGroup.headers.map((column: any) => (
                                    <th {...column.getHeaderProps(column.getSortByToggleProps())}>
                                        {column.render('Header')}
                                        <span>{column.isSorted ? (column.isSortedDesc ? ' 🔽' : ' 🔼') : ''}</span>
                                    </th>
                                ))}
                            </tr>
                        ))}
                        </thead>
                        <tbody {...getTableBodyProps()}>
                        {page.map((row: any, i: any) => {
                            prepareRow(row);
                            return (
                                <tr {...row.getRowProps()}>
                                    {row.cells.map((cell: any) => {
                                        return (
                                            <td {...cell.getCellProps()} >
                                                {cell.render('Cell')}
                                            </td>
                                        );
                                    })}
                                </tr>
                            );
                        })}
                        </tbody>
                    </Table>
                    <Pagination>
                        <Pagination.First key={"first"} onClick={() => gotoPage(0)}
                                          disabled={!canPreviousPage}></Pagination.First>

                        <Pagination.Prev key={"previus"} onClick={() => previousPage()}
                                         disabled={!canPreviousPage}></Pagination.Prev>
                        <Pagination.Next key={"nest"} onClick={() => nextPage()}
                                         disabled={!canNextPage}></Pagination.Next>

                        <Pagination.Last key={"last"} onClick={() => gotoPage(pageCount - 1)}
                                         disabled={!canNextPage}></Pagination.Last>
                    </Pagination>

                </Col>
            </Row>

            <VehicleCrudModal/>
        </>
    );
};
export default VehicleTable;

