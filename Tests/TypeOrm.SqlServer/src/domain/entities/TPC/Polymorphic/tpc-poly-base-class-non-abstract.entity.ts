import { TpcPoly_RootAbstract } from './tpc-poly-root-abstract.entity';
import { Entity, PrimaryGeneratedColumn, Column } from 'typeorm';

@Entity('tpc_poly_base_class_non_abstract')
export class TpcPoly_BaseClassNonAbstract extends TpcPoly_RootAbstract {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  baseField: string;

}