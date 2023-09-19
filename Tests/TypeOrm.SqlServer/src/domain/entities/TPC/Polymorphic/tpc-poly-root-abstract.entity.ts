import { TpcPoly_RootAbstract_Aggr } from './tpc-poly-root-abstract-aggr.entity';
import { TpcPoly_RootAbstract_Comp } from './tpc-poly-root-abstract-comp.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne, OneToOne, JoinColumn } from 'typeorm';

@Entity('tpc_poly_root_abstract')
export abstract class TpcPoly_RootAbstract {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  abstractField: string;

  @Column({ nullable: true })
  polyRootabstractAggrId?: string;

  @ManyToOne(() => TpcPoly_RootAbstract_Aggr, { cascade: ['insert', 'update'] })
  poly_RootAbstract_Aggr?: TpcPoly_RootAbstract_Aggr;

  @OneToOne(() => TpcPoly_RootAbstract_Comp, { cascade: true, eager: true })
  @JoinColumn()
  poly_RootAbstract_Comp?: TpcPoly_RootAbstract_Comp;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}